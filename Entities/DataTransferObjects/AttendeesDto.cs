﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
   public  class AttendeesDto
    {
        public int Id { get; set; }       
        public string FirstName { get; set; }      
        public string LastName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }

        }
    }
}
