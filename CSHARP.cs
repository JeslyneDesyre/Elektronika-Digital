using System;
using System.Threading;

enum FurnaceState
{
    IDLE,
    STABLE,
    WARNING,
    CRISIS
}

class FurnaceController
{
    public FurnaceState CurrentState = FurnaceState.IDLE;

    public void UpdateState(int S1, int S2, int S4, int S6)
    {
        if (S1 == 0 || S4 == 0)
        {
            CurrentState = FurnaceState.CRISIS;
        }
        else if (S2 == 0)
        {
            CurrentState = FurnaceState.WARNING;
        }
        else if (S6 == 0)
        {
            CurrentState = FurnaceState.STABLE; 
        }
        else
        {
            CurrentState = FurnaceState.STABLE;
        }
    }

    public void PrintActuators()
    {
        Console.WriteLine("\n=== ACTUATORS ===");

        if (CurrentState == FurnaceState.IDLE)
        {
            Console.WriteLine("Fuel Valve : 0");
            Console.WriteLine("Draft Fan  : 0");
            Console.WriteLine("Damper     : 0");
            Console.WriteLine("Pump       : 0");
            Console.WriteLine("SCR        : 0");
        }
        else if (CurrentState == FurnaceState.STABLE)
        {
            Console.WriteLine("Fuel Valve : 1");
            Console.WriteLine("Draft Fan  : 1");
            Console.WriteLine("Damper     : 1");
            Console.WriteLine("Pump       : 1");
            Console.WriteLine("SCR        : 1");
        }
        else if (CurrentState == FurnaceState.WARNING)
        {
            Console.WriteLine("Fuel Valve : 1");
            Console.WriteLine("Draft Fan  : 0  (Draft rendah)");
            Console.WriteLine("Damper     : 1");
            Console.WriteLine("Pump       : 1");
            Console.WriteLine("SCR        : 1");
        }
        else if (CurrentState == FurnaceState.CRISIS)
        {
            Console.WriteLine("Fuel Valve : 0  (Emergency Shutdown)");
            Console.WriteLine("Draft Fan  : 1");
            Console.WriteLine("Damper     : 1");
            Console.WriteLine("Pump       : 1");
            Console.WriteLine("SCR        : 0");
        }
    }
}

class Program
{
    static void Main()
    {
        FurnaceController f = new FurnaceController();
        Random rnd = new Random();

        Console.WriteLine("=== FURNACE CONTROL SYSTEM AUTOMATIC SIMULATION ===");
        Console.WriteLine("Tekan CTRL + C untuk stop.\n");

        while (true)
        {
            int S1 = rnd.Next(0, 2); // Flow
            int S2 = rnd.Next(0, 2); // Draft
            int S4 = rnd.Next(0, 2); // Temperature
            int S6 = rnd.Next(0, 2); // Spectrometer

            Console.WriteLine("=====================================");
            Console.WriteLine($"S1 Flow         = {S1}");
            Console.WriteLine($"S2 Draft        = {S2}");
            Console.WriteLine($"S4 Temperature  = {S4}");
            Console.WriteLine($"S6 Spectrometer = {S6}");

            f.UpdateState(S1, S2, S4, S6);

            Console.WriteLine($">>> STATE = {f.CurrentState}");
            f.PrintActuators();

            Thread.Sleep(1000);
        }
    }
}
