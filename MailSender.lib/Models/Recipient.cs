

using MailSender.lib.Models.Base;
using System;
using System.ComponentModel;

namespace MailSender.Models
{
    public class Recipient : Person, IDataErrorInfo
    {
       
        public override string Name 
        { 
            get => base.Name;
            set
            {
                //if (value is null)               
                //    throw new ArgumentNullException(nameof(value));
               
                //if (value == "")                
                //    throw new ArgumentException("Имя не может быть пустой строкой", nameof(value));
                
                if (value == "QWE")
                    throw new ArgumentException("Запрещено вводить QWE!", nameof(value));

                base.Name = value;
            }
        }

        string IDataErrorInfo.Error { get; }
        string IDataErrorInfo.this[string PropertyName]
        {
            get
            {
                switch (PropertyName)
                {
                    default: return null;
                    case nameof(Name):
                        var name = Name;
                        if(name is null) return "Имя не может быть пустой строкой";
                        if (name.Length <= 2) return "Имя не должно быть короче двух символов";
                        if(name.Length > 20) return "Имя не должно быть длиннее двадцати символов";
                        return null;
                    case nameof(Address):
                        return null;
                }
            }
        }

        
    }
    
}
