// See https://aka.ms/new-console-template for more information
using GameStateIntegrator;
using System.Reactive.Linq;

Console.WriteLine("Hello, World!");

var listener = new GsListener();
listener.ProviderObserver.Subscribe(x => Console.WriteLine(x.Name));
listener.PlayerObserver.Subscribe(x => Console.WriteLine(x.Name));
listener.MapObserver.Subscribe(x => Console.WriteLine(x.Name));
listener.Start();

Console.ReadKey();