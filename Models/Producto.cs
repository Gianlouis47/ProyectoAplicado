using System.ComponentModel.DataAnnotations;

namespace GoldClub.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        public int CategoriaId { get; set; }

        public int? ProveedorId { get; set; }

        public int? UbicacionId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El código SKU es obligatorio")]
        [StringLength(50)]
        public string CodigoSku { get; set; } = string.Empty;

        public string? Marca { get; set; }

        public string? Modelo { get; set; }

        [Required(ErrorMessage = "El precio de compra es obligatorio")]
        [Range(0, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio")]
        [Range(0, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioVenta { get; set; }

        public int Stock { get; set; }

        public int StockMinimo { get; set; }

        [Required(ErrorMessage = "La unidad de medida es obligatoria")]
        [StringLength(20)]
        public string UnidadMedida { get; set; } = "UN";

        public bool Activo { get; set; } = true;

        public DateTime CreadoEn { get; set; } = DateTime.Now;

        public int? UsuarioCreadorId { get; set; }
        public bool SkuAutogenerado { get; set; }
        public DateTime? ModificadoEn { get; set; }
        public string? CodigoBarras { get; set; }
        public string[]? Tags { get; set; }

        public Categoria? Categoria { get; set; }
        public Proveedor? Proveedor { get; set; }
        public Ubicacion? Ubicacion { get; set; }
    }
}