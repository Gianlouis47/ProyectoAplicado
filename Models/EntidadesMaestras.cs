using System.ComponentModel.DataAnnotations;

namespace GoldClub.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.Now;
    }

    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(150)]
        public string NombreEmpresa { get; set; } = string.Empty;
        
        public string? ContactoNombre { get; set; }
        
        public string? Telefono { get; set; }
        
        public string? Correo { get; set; }
        
        public string? Direccion { get; set; }
        
        public string? Rnc { get; set; }
        
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.Now;
    }

    public class Ubicacion
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;
        
        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime CreadoEn { get; set; } = DateTime.Now;
    }
}