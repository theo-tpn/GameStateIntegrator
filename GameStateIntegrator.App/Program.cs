// See https://aka.ms/new-console-template for more information
using GameStateIntegrator;
using System.Reactive.Linq;

Console.WriteLine("Hello, World!");

var listener = new GsListener();
listener.Start();

Console.ReadKey();