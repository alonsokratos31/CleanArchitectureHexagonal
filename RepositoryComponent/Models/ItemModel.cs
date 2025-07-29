﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryComponent.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<NoteModel> Notes { get; set; }

    }
}
