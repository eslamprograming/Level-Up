namespace Level_up.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Q_choice
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Q_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string multchoice { get; set; }
        [JsonIgnore]


        public virtual Quction Quction { get; set; }
    }
}
