//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecursosHumanos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permiso
    {
        public int ID { get; set; }
        public Nullable<int> FK_Empleado { get; set; }
        public string Comentario { get; set; }
        public Nullable<System.DateTime> Fecha_Entrada { get; set; }
        public Nullable<System.DateTime> Fecha_Salida { get; set; }
    
        public virtual Empleados Empleados { get; set; }
    }
}
