using TrabalhoVendinha.Controllers;

class Program
{
    static void Main(string[] args)
    {
        var controller = new ClienteController();
        controller.Executar();
    }
}