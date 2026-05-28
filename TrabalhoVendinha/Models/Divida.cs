using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoVendinha.Models
{
    public class Divida
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }

        public bool Paga { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataPagamento { get; set; }

        public Cliente Cliente { get; set; }

        }       
   }

