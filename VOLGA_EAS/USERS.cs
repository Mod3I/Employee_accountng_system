//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VOLGA_EAS
{
    using System;
    using System.Collections.Generic;
    
    public partial class USERS
    {
        public int USER_ID { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_NAME { get; set; }
        public string USER_FIRST_NAME { get; set; }
        public string USER_SECOND_NAME { get; set; }
        public string USER_PATRONYMIC { get; set; }
        public string USER_DATE_OF_BIRTH { get; set; }
        public Nullable<int> USER_EMPLOYEE { get; set; }
        public string USER_DATE_OF_ADDITION { get; set; }
    
        public virtual EMPLOYEES EMPLOYEES { get; set; }
    }
}
