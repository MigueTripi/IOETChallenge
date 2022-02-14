using IOETChallenge.Business;
using IOETChallenge.Domain;
using IOETChallenge.DTO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.WithBusinessBinding())
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;

var business = provider.GetRequiredService<IEmployeePaymentBusiness>();

//TODO: store this value in config file.
var minimumRowsToProcess = 5;

Console.WriteLine("Hi reviewer!");
Console.WriteLine($"The file content has to have at least {minimumRowsToProcess} rows and the following format:");
Console.WriteLine("WORKER=DDHH:mm-HH:mm,");
Console.WriteLine("WORKER -> Worker's name");
Console.WriteLine("DD -> First two characters of the day. i.e. MO for monday");
Console.WriteLine("HH -> Hours");
Console.WriteLine("mm -> Minutes");
Console.WriteLine("- -> Separator between both times");
Console.WriteLine(", -> Separator between differents days and times");
Console.WriteLine("EXAMPLE -> RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00");

do
{
    Console.WriteLine("Please provide full file name to process:");
    var fileName = Console.ReadLine();
    var result = business.CalculateEmployeePayments(fileName, minimumRowsToProcess);
    if (result.Success)
    {
        if (result.ErrorCode != (byte)EmployeePaymentOperationErrorCodesDTO.epoecOK)
        {
            Console.WriteLine($"Following rows were not processed completely. Please review entered format:");
            result.RowsWithErrors.ForEach(x => Console.WriteLine(x));
            Console.WriteLine();
        }

        Console.BackgroundColor = ConsoleColor.Green;
        result.EmployeePayments.ForEach(x =>
            Console.WriteLine($"The amount to pay {x.Name} is: {x.Amount} {x.Currency}"));
        
    }
    else
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine($"There were errors in the process: {result.ErrorMessage}");
    }

    Console.BackgroundColor = ConsoleColor.Black;
    Console.WriteLine("Would you like to calculate other file?");

} while (Console.ReadLine() == "Y");

Console.WriteLine("GoodBye!!!");

