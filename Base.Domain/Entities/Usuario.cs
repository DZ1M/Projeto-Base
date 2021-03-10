using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base.Domain.Entities
{
    [Table("usuarios", Schema = "public")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("email"), MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [Column("senha"), MaxLength(200)]
        public string Senha { get; set; }

        //[Column("id_empresa")]
        //[InverseProperty("id")]
        //[ForeignKey("Empresas")]
        public Guid IdEmpresa { get; set; }
        //public virtual Empresas Empresas { get; set; }
    }
}
