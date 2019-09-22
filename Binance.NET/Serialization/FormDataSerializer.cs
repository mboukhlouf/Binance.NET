using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Binance.Serialization
{
    class FormDataSerializer
    {
        public static String Serialize(Object obj)
        {
            if (!Attribute.IsDefined(obj.GetType(), typeof(FormData)))
            {
                throw new InvalidFormDataException();
            }

            var fields = obj.GetType().GetFields().Cast<MemberInfo>()
                .Concat(obj.GetType().GetProperties())
                .Where(member => Attribute.IsDefined(member, typeof(FormField)));

            var fieldsString = fields.Where(field =>
            {
                if (field is PropertyInfo)
                {
                    var propertyInfo = (PropertyInfo)field;
                    return propertyInfo.GetValue(obj) != null;
                }
                else
                {
                    var fieldInfo = (FieldInfo)field;
                    return fieldInfo.GetValue(obj) != null;
                }
            })
            .Select(field =>
            {
                FormField attribute = (FormField)Attribute.GetCustomAttribute(field, typeof(FormField));
                String fieldName = attribute.Name ?? field.Name;
                String fieldValue;
                if (field is PropertyInfo)
                {
                    var propertyInfo = (PropertyInfo)field;
                    fieldValue = propertyInfo.GetValue(obj).ToString();
                }
                else
                {
                    var fieldInfo = (FieldInfo)field;
                    fieldValue = fieldInfo.GetValue(obj).ToString();
                }

                return $"{fieldName}={fieldValue}";
            });

            return String.Join("&", fieldsString);
        }
    }
}
