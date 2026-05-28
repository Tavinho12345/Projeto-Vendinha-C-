using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrabalhoVendinha.Models;
using System.Linq;
using TrabalhoVendinha.Data;
using Microsoft.EntityFrameworkCore;


namespace TrabalhoVendinha.Service
{
    internal class ClienteService
    {
        // metodo de cadastro dos clientes
        public void CadastrarCliente(Cliente cliente)
        {

            using var db = new AppDbContext();


            var contexto = new ValidationContext(cliente);


            var resultados = new List<ValidationResult>();

            bool valido = Validator.TryValidateObject(
                cliente,
                contexto,
                resultados,
                true
            );

            if (!valido)
            {
                foreach (var erro in resultados)
                {
                   Console.WriteLine(erro.ErrorMessage);
                }

                return;
            }

            // verificando se ja existe um cpf igual
            if (db.Clientes.Any(c => c.CPF == cliente.CPF))
            {
      
                Console.WriteLine("CPF já cadastrado.");
                return;
            }


            // adiciona clientes na lista
            db.Clientes.Add(cliente);
            db.SaveChanges();

            Console.WriteLine("Cliente cadastrado com sucesso!");
        }


        // metodo para listar clientes
        public List<Cliente> ListarClientes()
        {
            using var db = new AppDbContext();
            
            return db.Clientes
                .Include(c => c.Dividas)
                .ToList();

        }
        // metodo para adicionar dividas


        public void AdicionarDivida(string cpf, decimal valor)
        {


            using var db = new AppDbContext();


            var cliente  = db.Clientes
                .Include(c => c.Dividas)
                .FirstOrDefault(c => c.CPF == cpf);

            // verirfica se o cliete ja existe
            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }


            // aqui verificamos se o cliente ja possui alguma divida em aberto
            bool possuiDividaAberta = cliente.Dividas.Any(d => d.Paga == false);

            if (possuiDividaAberta)
            {
                Console.WriteLine("Cliente já possui dívida em aberto.");
                return;
            }

            // aqui cria uma divida nova caso nao possua uma divida 
            var divida = new Divida
            {
                Valor = valor,
                Paga = false,
                DataCriacao = DateTime.Now,
                Cliente = cliente
            };

           db.Dividas.Add(divida);
           db.SaveChanges();

           

            Console.WriteLine("Dívida adicionada com sucesso.");

        }


        // metodo para pagar a divida
        public void PagarDivida(string cpf)
        {
            using var db = new AppDbContext();

            var cliente = db.Clientes
                .Include(c => c.Dividas)
                .FirstOrDefault(c => c.CPF == cpf);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            var divida = cliente.Dividas.FirstOrDefault(d => d.Paga == false);

            if (divida == null)
            {
                Console.WriteLine("Cliente não possui dívida em aberto.");
                return;
            }


            // marca a divida como paga
            divida.Paga = true;

            // definindo a data do pagamento
            divida.DataPagamento = DateTime.Now;

            db.SaveChanges();

            Console.WriteLine("Dívida paga com sucesso.");

            }
            
        // listar cliente por oredem de quem deve mais
            public List<Cliente> ListarClientePorOrdem()
            {

                using var db = new AppDbContext();

                return db.Clientes
                    .Include(c => c.Dividas)
                    .ToList()
                    .OrderByDescending(c => c.TotalDividas)
                    .ToList();


        }
        
            // busca por nome
            public List<Cliente> BuscarClientes(string nome)
            {
                using var db = new AppDbContext();

                return db.Clientes
                    .Include(c => c.Dividas)
                    .ToList()
                    .Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(c => c.TotalDividas)
                    .ToList();

        }
            
            // paginacao (carregar de 10 em 10)

            public List<Cliente> ListarClientesPorPaginacao(int pagina)
            {
                int itensPorPagina = 10;

                using var db = new AppDbContext();

                return db.Clientes
                    .Include(c => c.Dividas)
                    .ToList()
                    .Skip((pagina - 1) * itensPorPagina)
                    .Take(itensPorPagina)
                    .ToList();

            }

         }   

    }



