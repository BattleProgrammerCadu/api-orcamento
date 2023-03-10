using api.DTOs;
using api.Models;

namespace api.Servicos;

public class BuilderServico<T>
{
    public static T Builder(object objectDTO)
    {
        var obj = Activator.CreateInstance<T>();
        
        foreach(var propDTO in objectDTO.GetType().GetProperties())
        {
            var propObject = obj?.GetType().GetProperty(propDTO.Name);
            if(propObject is not null)
            {
                propObject.SetValue(obj, propDTO.GetValue(objectDTO));
            }
        }
        return obj;
    }
}