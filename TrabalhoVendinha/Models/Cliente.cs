using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

namespace TrabalhoVendinha.Models
{
    public class Cliente
    {  

        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [MinLength(1, ErrorMessage = "Nome não pode estar vazio")]
        [RegularExpression("^[A-Z][A-zA-z]+ [A-Z][A-zA-z ]+[^ ]$")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "CPF obrigatório")]
        
        [RegularExpression("^[0-9]+$",
        ErrorMessage = "CPF deve conter apenas números")]
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Email obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public List<Divida> Dividas { get; set; } = new List<Divida>();


        public decimal TotalDividas
        {
            get
            {
                return Dividas
                    .Where(d => d.Paga == false)
                    .Sum(d => d.Valor);
            }
        }


        // calcula idade automaticamente
        public int Idade
        {
            get
            {
                var hoje = DateTime.Today;

                var anos = hoje.Year - DataNascimento.Year;

                var aniversarioEsseAno =
                    DataNascimento.AddYears(anos);

                if (hoje < aniversarioEsseAno)
                {
                    anos--;
                }

                return anos;
            }
        }


        // mostra os dados do cliente
        public virtual void PrintDados()
        {
            Console.WriteLine("Nome: {0}", Nome);
            Console.WriteLine("CPF: {0}", CPF);
            Console.WriteLine("Data de Nascimento: {0:dd/MM/yyyy}", DataNascimento);
            Console.WriteLine("Idade: {0}", Idade);
            Console.WriteLine("Email: {0}", Email);
            Console.WriteLine("Total Dívidas: R$ {0}", TotalDividas);
        }
    }
}