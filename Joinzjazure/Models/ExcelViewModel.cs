﻿using Joinzjazure.Data;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Joinzjazure.Models
{
    [DataContract]
    public class ExcelViewModel
    {
        public ExcelViewModel(FormEntity entity)
        {
            Gender = entity.Gender;
            Email = entity.Email;
            Phone = entity.Phone;
            QQ = entity.QQ;
            Weibo = entity.Weibo;
            Description = entity.Description;
            Name = entity.RowKey;
            Class = int.Parse(entity.PartitionKey) % 100;
            Grade = int.Parse(entity.PartitionKey) / 100;

            Groups = string.Join(",", (from g in GroupXmlStore.GetAll()
                                       where (g.Value & entity.Groups) != 0
                                       select g.Name).ToArray());
        }

        [DataMember]
        public int Class { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool Gender { get; set; }

        [DataMember]
        public int Grade { get; set; }

        [DataMember]
        public string Groups { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string QQ { get; set; }

        
        [DataMember]
        public string Weibo { get; set; }
    }
}
