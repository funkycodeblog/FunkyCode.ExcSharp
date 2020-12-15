using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FunkyCode.ExcSharp.Engine.Tools
{
    public class PropertyMapper
    {
        
        public static void MapProperties(object source, object target)
        {
            if (source == null)
                return;

            var sourceType = source.GetType();
            var targetType = target.GetType();

            var isList = sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == typeof(List<>);

            if (isList)
            {
                var sourceCollection = (IList) source;

                var addMethod = targetType.GetMethod("Add");

                for (var index = 0; index < sourceCollection.Count; index++)
                {
                    var sourceItem = sourceCollection[index];
                    var sourceItemType = sourceItem.GetType();

                    var destinationItem = Activator.CreateInstance(sourceItemType);

                    MapProperties(sourceItem, destinationItem);
                    
                    object[] methodParameters = {destinationItem};
                    addMethod.Invoke(target, methodParameters);
                }

                return;
            }

            var targetProperties = target.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var sourceName = sourceProperty.Name;
                var sourcePropertyType = sourceProperty.PropertyType;

                var matchedProperty = targetProperties
                    .FirstOrDefault(p =>
                        p.CanWrite &&
                        p.Name == sourceName &&
                        p.PropertyType == sourcePropertyType);

                if (null == matchedProperty) continue;

                var sourceValue = sourceProperty.GetValue(source, null);

                if (sourcePropertyType.IsClass && !(sourcePropertyType == typeof(string)))
                {
                    var destinationValue = matchedProperty.GetValue(target) ?? Activator.CreateInstance(matchedProperty.PropertyType);

                    MapProperties(sourceValue, destinationValue);
                    matchedProperty.SetValue(target, destinationValue);

                    continue;
                }

                matchedProperty.SetValue(target, sourceValue, null);
            }
        }
    }

}
