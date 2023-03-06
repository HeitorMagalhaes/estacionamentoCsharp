using System;
using System.Collections.Generic;
using System.Linq;
// To execute C#, please define "static void Main" on a class
// named Solution.


public enum TipoVeiculo
{
    Moto = 1,
    Carro = 2,
    Van = 3
}
public enum TipoVaga
{
    Pequena = 1,
    Media = 2, 
    Grande = 3
}

class Solution
{

    static void Main(string[] args)
    {

        List<Vaga> vagas = new List<Vaga>();
        // Fiz um estacionamento com 100 vagas sendo 30 grandes 50 de carro e 20 de moto
        for (int i = 1; i <= 30; i++)
        {
            Vaga vaga = new Vaga(i, TipoVaga.Grande);
            vagas.Add(vaga);
        }

        for (int i = 31; i <= 80; i++)
        {
            Vaga vaga = new Vaga(i, TipoVaga.Media);
            vagas.Add(vaga);
        }

        for (int i = 81; i <= 100; i++)
        {
            Vaga vaga = new Vaga(i, TipoVaga.Pequena);
            vagas.Add(vaga);
        }

        Console.WriteLine("Olá bem vindo(a) ao nosso estacionamento!");

        int contadorMenu = 0;

        while(contadorMenu < 1)
        {
            Console.WriteLine("Escolha uma das opções disponíveis:");
            Console.WriteLine("1 - Estacionar seu veículo");
            Console.WriteLine("2 - Ver quantas vagas estão disponíveis");
            Console.WriteLine("3 - Ver quantas vagas estão ocupadas");
            Console.WriteLine("4 - Ver quantas vagas existem no estacionamento");
            Console.WriteLine("5 - Ver detalhes de uma vaga específica");
            Console.WriteLine("6 - Ver detalhes específicos");
            Console.WriteLine("9 - Sair");

            string opcao;

            opcao = Console.ReadLine();          

            if (opcao == "1")
            {
                string placaVeiculo = "";
                string modeloVeiculo = "";
                TipoVeiculo tipoVeiculo = TipoVeiculo.Carro;

                // Valida a entrada da placa
                while (string.IsNullOrWhiteSpace(placaVeiculo))
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine($"Digite a placa do seu veiculo:");
                    Console.WriteLine("==============================");
                    placaVeiculo = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(placaVeiculo))
                    {
                        Console.WriteLine("==============================");
                        Console.WriteLine($"Placa inválida, digite novamente.");
                        Console.WriteLine("==============================");
                    }
                }

                // Valida a entrada do modelo
                while (string.IsNullOrWhiteSpace(modeloVeiculo))
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine($"Digite o modelo do seu veiculo:");
                    Console.WriteLine("==============================");
                    modeloVeiculo = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(modeloVeiculo))
                    {
                        Console.WriteLine("==============================");
                        Console.WriteLine($"Modelo inválido, digite novamente.");
                        Console.WriteLine("==============================");
                    }
                }


                int numTipoCarro = 0;
                bool entradaValida = false;

                while (!entradaValida)
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine($"Digite o tipo do seu veiculo sendo 1-Moto, 2-Carro, 3-Van:");
                    Console.WriteLine("==============================");
                    string entrada = Console.ReadLine();

                    if (int.TryParse(entrada, out numTipoCarro) && (numTipoCarro >= 1 && numTipoCarro <= 3))
                    {
                        entradaValida = true;

                        if (numTipoCarro == 1)
                        {
                            tipoVeiculo = TipoVeiculo.Moto;
                        }
                        else if (numTipoCarro == 2)
                        {
                            tipoVeiculo = TipoVeiculo.Carro;
                        }
                        else if (numTipoCarro == 3)
                        {
                            tipoVeiculo = TipoVeiculo.Van;
                        }
                    }
                    else
                    {
                        Console.WriteLine("==============================");
                        Console.WriteLine($"Tipo de veiculo inválido digite novamente:");
                    }
                }

                // Cria um objeto Veiculo com as informações do veículo
                Veiculo veiculo = new Veiculo(placaVeiculo, modeloVeiculo, tipoVeiculo)
                {
                    Modelo = modeloVeiculo,
                    Placa = placaVeiculo,
                    TipoVeiculo = tipoVeiculo
                };

                Vaga vagaDisponivel = vagas.FirstOrDefault(v => (int)v.TipoVaga == (int)veiculo.TipoVeiculo && !v.Ocupada);

                // Verifica se encontrou uma vaga disponível
                if (vagaDisponivel != null)
                {
                    // Ocupa a vaga com o veículo
                    vagaDisponivel.Ocupada = true;
                    vagaDisponivel.VeiculoEstacionado = veiculo;

                    Console.WriteLine("==============================");
                    Console.WriteLine($"Veículo {veiculo.Modelo} da placa {veiculo.Placa} estacionado na vaga {vagaDisponivel.NumeroVaga}.");
                    Console.WriteLine("==============================");
                }
                else
                {
                    if(veiculo.TipoVeiculo == TipoVeiculo.Moto)
                    {
                        Vaga vagaNova = vagas.FirstOrDefault(v => !v.Ocupada);

                        // Ocupa a vaga com o veículo
                        vagaNova.Ocupada = true;
                        vagaNova.VeiculoEstacionado = veiculo;

                        Console.WriteLine("==============================");
                        Console.WriteLine($"Veículo {veiculo.Modelo} da placa {veiculo.Placa} estacionado na vaga {vagaNova.NumeroVaga}, uma vaga {vagaNova.TipoVaga}, pois todas as vagas pequenas para motos estão ocupadas.");
                        Console.WriteLine("==============================");
                    }

                    if (veiculo.TipoVeiculo == TipoVeiculo.Carro)
                    {
                        Vaga vagaNova = vagas.FirstOrDefault(v => v.TipoVaga  == TipoVaga.Grande && !v.Ocupada);

                        // Ocupa a vaga com o veículo
                        vagaNova.Ocupada = true;
                        vagaNova.VeiculoEstacionado = veiculo;

                        Console.WriteLine("==============================");
                        Console.WriteLine($"Veículo {veiculo.Modelo} da placa {veiculo.Placa} estacionado na vaga {vagaNova.NumeroVaga}, uma vaga {vagaNova.TipoVaga}, pois todas as vagas médias para carros estão ocupadas.");
                        Console.WriteLine("==============================");
                    }

                    if (veiculo.TipoVeiculo == TipoVeiculo.Van)
                    {
                        //Pega 3 vagas médias disponíveis
                        List<Vaga> vaganova = vagas
                        .Where(v => v.TipoVaga == TipoVaga.Media && !v.Ocupada)
                        .Take(3)
                        .ToList();

                        //Verifica se há 3 vagas médias disponíveis
                        if (vaganova.Count != 3)
                        {
                            Console.WriteLine("Não há vagas disponíveis para vans.");
                        }
                        else
                        {
                            // Ocupa a vaga com o veículo
                            foreach (var vaga in vaganova)
                            {
                                // Ocupa a vaga com o veículo
                                vaga.Ocupada = true;
                                vaga.VeiculoEstacionado = veiculo;

                            }

                            Console.WriteLine("==============================");
                            Console.WriteLine($"Veículo {veiculo.Modelo} da placa {veiculo.Placa} estacionado nas vagas {vaganova[0].NumeroVaga}, {vaganova[1].NumeroVaga}, {vaganova[2].NumeroVaga}, três vagas médias, pois todas as vagas grandes para vans estão ocupadas.");
                            Console.WriteLine("==============================");
                        }
                        
                    }


                }
            }
            else if (opcao == "2")
            {
                // Conta quantas vagas estão vazias
                int vagasVazias = vagas.Count(v => !v.Ocupada);

                // Verifica se todas as vagas estão vazias
                if (vagas.All(v => !v.Ocupada))
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine("O estacionamento está vazio.");
                    Console.WriteLine("==============================");
                }
                else
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine($"Temos {vagasVazias} vagas disponíveis.");
                    Console.WriteLine("==============================");
                }

                    
            }else if(opcao == "3")
            {
                // Conta quantas vagas estão ocupadas
                int vagasOcupadas = vagas.Count(v => v.Ocupada);

                // Verifica se todas as vagas estão ocupadas
                if (vagas.All(v => v.Ocupada))
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine("O estacionamento está cheio.");
                    Console.WriteLine("==============================");
                }
                else
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine($"Temos {vagasOcupadas} vagas ocupadas.");
                    Console.WriteLine("==============================");
                }

                
            }
            else if (opcao == "4")
            {
                // Conta as vagas totais
                int vagasTotais = vagas.Count();

                Console.WriteLine($"Temos {vagasTotais} vagas no nosso estacionamento.");
            }else if(opcao == "5")
            {
                Console.WriteLine("==============================");

                Console.WriteLine($"Temos vagas numeradas de 1 a 100, sobre qual vaga deseja saber mais?");

                Console.WriteLine("==============================");

                string numero = Console.ReadLine();

                //Procura os dados sobre a vaga selecionada
                Vaga vaga = vagas.FirstOrDefault(v => v.NumeroVaga == Convert.ToInt32(numero));

                Veiculo veiculoVaga;

                // Verifica se encontrou uma vaga disponível
                if (vaga != null)
                {
                    if (vaga.Ocupada)
                    {
                        veiculoVaga = vaga.VeiculoEstacionado;

                        Console.WriteLine("==============================");

                        Console.WriteLine($"A vaga {vaga.NumeroVaga} é uma vaga do tipo {vaga.TipoVaga}, e está ocupada pelo veículo {veiculoVaga.Modelo} da placa {veiculoVaga.Placa}.");

                        Console.WriteLine("==============================");
                    }
                    else
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"A vaga {vaga.NumeroVaga} é uma vaga do tipo {vaga.TipoVaga}, e está disponível.");

                        Console.WriteLine("==============================");
                    }

                    
                }
                else
                {
                    Console.WriteLine("==============================");

                    Console.WriteLine($"Vaga inválida.");

                    Console.WriteLine("==============================");
                }

            }
            else if (opcao == "6")
            {
                Console.WriteLine("==============================");

                Console.WriteLine("Escolha uma das opções disponíveis:");
                Console.WriteLine("1 - Ver quantidade de vagas para motos disponíveis.");
                Console.WriteLine("2 -Ver quantidade de vagas para carros disponíveis.");
                Console.WriteLine("3 - Ver quantidade de vagas para vans disponíveis.");

                Console.WriteLine("==============================");

                string valor = Console.ReadLine();

                if(valor == "1")
                {
                    //Conta quantas vagas de moto estão disponíveis
                    int quantidadeDeVagasDisponiveisParaMotos = vagas.Count(v => v.TipoVaga == TipoVaga.Pequena && !v.Ocupada);

                    if(quantidadeDeVagasDisponiveisParaMotos == 20)
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"Todas as vagas de moto estão disponíveis.");

                        Console.WriteLine("==============================");
                    }else if(quantidadeDeVagasDisponiveisParaMotos == 0)
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"Todas as vagas de moto estão ocupadas.");

                        Console.WriteLine("==============================");
                    }
                    else
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"A quantidade de vagas para motos disponíveis é {quantidadeDeVagasDisponiveisParaMotos}.");

                        Console.WriteLine("==============================");
                    }
                }
                else if (valor == "2")
                {
                    //Conta quantas vagas de carro estão disponíveis
                    int quantidadeDeVagasDisponiveisParaCarros = vagas.Count(v => v.TipoVaga == TipoVaga.Media && !v.Ocupada);

                    if (quantidadeDeVagasDisponiveisParaCarros == 50)
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"Todas as vagas de carro estão disponíveis.");

                        Console.WriteLine("==============================");
                    }
                    else if (quantidadeDeVagasDisponiveisParaCarros == 0)
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"Todas as vagas de carro estão ocupadas.");

                        Console.WriteLine("==============================");
                    }
                    else
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"A quantidade de vagas para carros disponíveis é {quantidadeDeVagasDisponiveisParaCarros}.");

                        Console.WriteLine("==============================");
                    }
                }
                else if(valor == "3")
                {
                    //Conta quantas vagas de vans estão disponíveis
                    int quantidadeDeVagasDisponiveisParaVans = vagas.Count(v => v.TipoVaga == TipoVaga.Grande && !v.Ocupada);

                    if (quantidadeDeVagasDisponiveisParaVans == 30)
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"Todas as vagas de carro estão disponíveis.");

                        Console.WriteLine("==============================");
                    }
                    else if (quantidadeDeVagasDisponiveisParaVans == 0)
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"Todas as vagas de carro estão ocupadas.");

                        Console.WriteLine("==============================");
                    }
                    else
                    {
                        Console.WriteLine("==============================");

                        Console.WriteLine($"A quantidade de vagas para carros disponíveis é {quantidadeDeVagasDisponiveisParaVans}.");

                        Console.WriteLine("==============================");
                    }
                }
                else
                {
                    Console.WriteLine("==============================");

                    Console.WriteLine($"Opção inválida.");

                    Console.WriteLine("==============================");
                }
            }
            else if (opcao == "9")
            {
                Console.WriteLine("==============================");

                Console.WriteLine($"Volte sempre :)");

                contadorMenu = 1;
            }
            else
            {
                Console.WriteLine("==============================");

                Console.WriteLine($"Opção inválida, tente novamente.");

                Console.WriteLine("==============================");
            }


        }

    }
}

public class Vaga
{
    public int NumeroVaga { get; set; }
    public TipoVaga TipoVaga { get; set; }
    public bool Ocupada { get; set; }
    public Veiculo VeiculoEstacionado { get; set; }

    public Vaga(int numeroVaga, TipoVaga tipoVaga)
    {
        NumeroVaga = numeroVaga;
        TipoVaga = tipoVaga;
        Ocupada = false;
    }
}

public class Veiculo
{
    public string Placa { get; set; }
    public string Modelo { get; set; }
    public TipoVeiculo TipoVeiculo { get; set; }

    public Veiculo(string placa, string modelo, TipoVeiculo tipoVeiculo)
    {
        Placa = placa;
        Modelo = modelo;
        TipoVeiculo = tipoVeiculo;
    }
}



