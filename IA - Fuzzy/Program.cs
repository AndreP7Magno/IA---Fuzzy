using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA___Fuzzy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aguarde enquanto estamos recolhendo as informações do usuário...");

            //LER TXT USUÁRIO - GUARDAR NUMA LISTA DE USUÁRIOS

            System.Threading.Thread.Sleep(5000);
            Console.Clear();

            MontaMenu();

        }

        public static void MontaMenu()
        {
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║ \tPor favor escolha a disciplina desejada. ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║ 1. = PROGRAMAÇÃO                               ║");
            Console.WriteLine("║ 2. = EXCLUIR CADASTRO                          ║");
            Console.WriteLine("║ 3. = EDITAR CADASTRO                           ║");
            Console.WriteLine("║ 4. = VOLTAR AO MENU                            ║");
            Console.WriteLine("║ 5. = VOLTAR AO MENU                            ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
            Console.WriteLine("");
            Console.Write("SUA OPÇÃO: ");
            var opcao = Console.ReadLine();

            MontaLogica(opcao);
        }

        public static void MontaLogica(string opcao)
        {
            Console.Clear();
            Console.WriteLine("Verificando opção fornecida...");

            switch (opcao)
            {
                case "1":
                    InformaUsuario();
                    break;
                case "2":
                    InformaUsuario();
                    break;
                case "3":
                    InformaUsuario();
                    break;
                case "4":
                    InformaUsuario();
                    break;
                case "5":
                    InformaUsuario();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Opção Inválida.");

                    break;
            }
        }

        public static void InformaUsuario(){
            Console.Clear();
            Console.WriteLine("Aguarde enquanto estamos montando a melhor dupla possível para seu trabalho...");
        }
    }
}
