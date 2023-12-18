List<string[]> lista = new List<string[]>();
List<string[]> contaCorrente = new List<string[]>();

while (true){
    Console.Clear();

Console.WriteLine($"""
Seja bem vindo a empresa Lina!
O que você deseja fazer?
1 - Cadastrar cliente
2 - Ver extrato cliente
3 - Crédito em conta
4 - Retirada
5 - Sair do sistema

""");
var opcao = Console.ReadLine()?.Trim();
Console.Clear();
bool sair = false;

switch(opcao){
    case "1":
    Console.Clear();
    cadastrarCliente();
    break;

    case "2":
    Console.Clear();
    mostrarContaCorrente();
    break;

    case "3":
    AdicionarCreditoCliente();
    Console.Clear();
    break;
    case "4":
    Console.Clear();
    FazendoDebitoCliente();
    break;
    case "5":
    sair = true;
    break;
    default: 
    Console.WriteLine("Opção Inválida");
    break;   
}

if(sair) break;
}

void mostrarContaCorrente()
{
    Console.Clear();
    if(lista.Count == 0 || contaCorrente.Count == 0)
    {
        mensagem("Não existe cliente ou não existe movimentações em conta corrente, cadastre o cliente e faça crédito em conta");
            return;
    }
    var cliente = capturaCliente();
    var contaCorrenteCliente = extratoCliente(cliente[0]);
    Console.Clear();
    Console.WriteLine("-----------------");
    foreach(var contaCorrente in contaCorrenteCliente)
    {
        Console.WriteLine("Data: " + contaCorrente[2]);
        Console.WriteLine("Valor: " + contaCorrente[1]);
        Console.WriteLine("--------------------");
    }
    Console.WriteLine($"""
    O valor total da conta do cliente {cliente[1]} é de:
    R$ {saldoCliente(cliente[0], contaCorrenteCliente)}
    """);

    Console.WriteLine("Digite enter para continuar");
    Console.ReadLine();
    Thread.Sleep(6000);
}

void listarClientesCadastrados(){
    if(lista.Count == 0){
        menuCadastraClienteSenaoExiste();
    }
    mostrarClientes(false, 0, "======== [ Selecione um cliente na lista ] ");
}

void mostrarClientes(
    bool sleep = true,
    int timerSleep = 2000,
    string header = "========== [ Lista de Clientes ] ===========" ){
    Console.Clear();
    Console.WriteLine(header);
    foreach(var cliente in lista){
        Console.WriteLine("ID: " + cliente[0]);
        Console.WriteLine("Nome: " + cliente[1]);
        Console.WriteLine("Telefone: " + cliente[2]);
        Console.WriteLine("Email: " + cliente[3]);
        Console.WriteLine("------------------");

        if(sleep){
            Thread.Sleep(timerSleep);
            Console.Clear();
        }

        Thread.Sleep(3000);
        Console.Clear();
    }
}

void cadastrarCliente(){
    var id = Guid.NewGuid();

    Console.WriteLine("Informe o nome do cliente");
    var nomeCliente = Console.ReadLine();

    Console.WriteLine($"Informe o telefone do {nomeCliente}");
    var telefone = Console.ReadLine();

    Console.WriteLine($"Informe o email do {nomeCliente}");
    var email = Console.ReadLine();

    if(lista.Count > 0){
        string[]? cli = lista.Find(c => c[2] == telefone);
            if(cli != null){
            mensagem($"Cliente já cadastrado com este telefone {telefone}, cadastre novamente ");
            cadastrarCliente();
            }
    }

    string[] cliente = new string[4];

    nomeCliente = null;

    cliente[0] = id.ToString();
    cliente[1] = nomeCliente ?? "[Sem nome]";
    cliente[2] = telefone != null ? telefone : "[Sem telefone]";
    cliente[3] = email ?? "[Sem email]";
    lista.Add(cliente);
    mensagem($""" {nomeCliente} cadastrado com sucesso """);

    void mensagem(string msg){
        Console.Clear();
        Console.WriteLine(msg);
        Thread.Sleep(2000);
    }
}



void FazendoDebitoCliente(){
    void AdicionarCreditoCliente(){
    Console.Clear();
    var cliente = capturaCliente();
    Console.Clear();
    Console.WriteLine("Digite o valor de retirada");
    double credito = Convert.ToDouble(Console.ReadLine());
    string[] creditoConta = new string[3];
    
    creditoConta[0] = cliente[0];
    creditoConta[1] = $"-{credito}";
    creditoConta[2] = DateTime.Now.ToString("dd/MM/yyyy  HH:MM");

    var idCliente = cliente[0];
    contaCorrente.Add(creditoConta);
    mensagem($"""
    Retirada realizada com sucesso!
    Saldo do Cliente {cliente[1]} é de R${saldoCliente(idCliente)}
    """);
    }
}


void AdicionarCreditoCliente(){
    Console.Clear();
    var cliente = capturaCliente();
    Console.Clear();
    Console.WriteLine("Digite o valor do crédito");
    double credito = Convert.ToDouble(Console.ReadLine());
    string[] creditoConta = new string[3];
    
    creditoConta[0] = cliente[0];
    creditoConta[1] = credito.ToString();
    creditoConta[2] = DateTime.Now.ToString("dd/MM/yyyy  HH:MM");

    contaCorrente.Add(creditoConta);
    var idCliente = cliente[0];
    mensagem($"""
    Credito adicionado com sucesso!
    Saldo do Cliente {cliente[1]} é de R${saldoCliente(idCliente)}
    """);
}

List<string[]> extratoCliente(string idCliente)
{
    
    var contaCorrenteCliente = contaCorrente.FindAll(c => c[0] == idCliente);
    if(contaCorrenteCliente.Count == 0) return new List<string[]>();

    return contaCorrenteCliente;
}

double saldoCliente(string idCliente, List<string[]> contaCorrenteCliente = null)
{
    if(contaCorrente == null)
    contaCorrente = extratoCliente(idCliente);

    return contaCorrenteCliente.Sum(cc => Convert.ToDouble(cc[1]));
}

string[] capturaCliente(){
    listarClientesCadastrados();
    Console.WriteLine("Digite o ID do Cliente");
    var idCliente = Console.ReadLine()?.Trim();
    string[]? cliente = lista.Find(c => c[0] == idCliente);

    if(cliente == null){
        mensagem("Cliente não encontrado na lista, digite o ID corretamente da lista de clientes");
        Console.Clear();

        menuCadastraClienteSenaoExiste();
        
        return capturaCliente();
    }

    return cliente;
}

void mensagem (string msg){
    Console.Clear();
    Console.WriteLine(msg);
    Thread.Sleep(1500);
}

void menuCadastraClienteSenaoExiste(){
    Console.WriteLine($"""
    O que você deseja fazer?
    1 - Cadastrar cliente
    2 - Voltar ao menu
    3 - Sair do sistema
    """);
    
    var opcao = Console.ReadLine()?.Trim();

    switch(opcao){
        case "1":
        cadastrarCliente();
        break;
        case "2":
        AdicionarCreditoCliente();
        break;
        case "3":
        System.Environment.Exit(0);
        break;
        default:
        Console.WriteLine("Opção inválida");
        break;
    }
}