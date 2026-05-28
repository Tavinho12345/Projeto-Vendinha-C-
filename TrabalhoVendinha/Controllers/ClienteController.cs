using System;
using System.Collections.Generic;
using System.Text;
using TrabalhoVendinha.Models;
using TrabalhoVendinha.Service;

namespace TrabalhoVendinha.Controllers
{
    public class ClienteController
    {

        private ClienteService clienteService = new ClienteService();

        public void Executar()
        {
            while (true)
            {
                Console.WriteLine("\n1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Clientes");
                Console.WriteLine("3 - Adicionar Divida");
                Console.WriteLine("4 - Pagar Divida");
                Console.WriteLine("5 - Buscar cliente por nome");
                Console.WriteLine("6 - Listar clientes por paginacao");
                Console.WriteLine("0 - Sair");

                Console.Write("Escolha: ");
                int opcao = int.Parse(Console.ReadLine());

                if (opcao == 1)
                {
                    CadastrarCliente();
                }
                else if (opcao == 2)
                {
                    ListarClientes();
                }

                else if (opcao == 3)
                {
                    Console.WriteLine("Digite o CPF cliente");
                    var cpf = Console.ReadLine();

                    Console.WriteLine("Digite o valor da divida");
                    decimal valor = decimal.Parse(Console.ReadLine());

                    clienteService.AdicionarDivida(cpf, valor);

                }


                else if (opcao == 4)
                {
                    Console.WriteLine("Digite o CPF do cliente:");
                    var cpf = Console.ReadLine();

                    clienteService.PagarDivida(cpf);
                }

                else if ( opcao == 5)
                {
                    Console.WriteLine("Digite o nome do cliete:");

                    var nome = Console.ReadLine();

                    var clientes =
                        clienteService.BuscarClientes(nome);

                    foreach (var cliente in clientes)
                    {
                        Console.WriteLine("============");

                        cliente.PrintDados();
                    }
                }

                else if (opcao == 6)
                {
                    Console.WriteLine("Digite a pagina");

                    int pagina = 
                        int.Parse(Console.ReadLine());

                    var clientes =
                         clienteService
                        .ListarClientesPorPaginacao(pagina);

                    foreach (var cliente in clientes)
                    {
                        Console.WriteLine("================");

                        cliente.PrintDados();
                    }

                }



                else if (opcao == 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Opção inválida.");
                }
            }
        }

        private void CadastrarCliente()
        {
            Console.WriteLine("Digite o nome:");
            var nome = Console.ReadLine();

            Console.WriteLine("Digite o CPF:");
            var cpf = Console.ReadLine();

            if (cpf.Length != 11)
            {
                Console.WriteLine("Cpf invalido");
                return;
            }


            Console.WriteLine("Digite o email:");
            var email = Console.ReadLine();

            if (!email.Contains("@"))
            {
                Console.WriteLine("Email invalido");
                return;
            }



            Console.WriteLine("Digite a data de nascimento:");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine());

            Cliente cliente = new Cliente();

            cliente.Nome = nome;
            cliente.CPF = cpf;
            cliente.Email = email;
            cliente.DataNascimento = dataNascimento;

            clienteService.CadastrarCliente(cliente);
        }

        private void ListarClientes()
        {
            var clientes = clienteService.ListarClientes();

            foreach (var cliente in clientes)
            {
                Console.WriteLine("=======================");
               
                cliente.PrintDados();


                if (cliente.Dividas.Count == 0)
                {
                    Console.WriteLine("Nenhuma divida");

                }
                else
                {
                    foreach (var divida in cliente.Dividas)
                    {
                        Console.WriteLine($"Valor da divida: R$ {divida.Valor}");
                        Console.WriteLine($"Data criação: {divida.DataCriacao}");

                        if (divida.Paga)
                        {
                            Console.WriteLine("Status: Paga");
                            Console.WriteLine($"Data pagamento: {divida.DataPagamento}");
                        }
                        else
                        {
                            Console.WriteLine("Status: Em aberto");
                        }
                    }

                }
            }
        }



    }
}
