namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DONGIA")]
    public partial class DONGIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONGIA()
        {
            HOADONs = new HashSet<HOADON>();
        }

        [Key]
        public int ID_DONGIA { get; set; }

        [Required]
        [StringLength(10)]
        public string MADONGIA { get; set; }

        public double DONGIADIEN { get; set; }

        public double DONGIANUOC { get; set; }

        public bool? TRANGTHAI { get; set; }

        public DateTime NGAYAPDUNG { get; set; }

        public bool? DAXOA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADON> HOADONs { get; set; }
    }
}
