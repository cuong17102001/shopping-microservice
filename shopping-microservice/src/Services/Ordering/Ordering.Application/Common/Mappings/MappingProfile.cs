using System.Reflection;
using AutoMapper;

namespace Ordering.Application.Common.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile(){
        ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyConfigurationsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        const string mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type type) => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapFromType);
        
        var types = assembly.GetExportedTypes()
        .Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod(mappingMethodName);

            if(methodInfo != null){
                methodInfo.Invoke(instance, new object[] { this });
            }else{
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();
                foreach (var interfaceType in interfaces)
                {
                    var interfaceMethodInfo = interfaceType.GetMethod(mappingMethodName);
                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}