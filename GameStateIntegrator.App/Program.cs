// See https://aka.ms/new-console-template for more information
using GameStateIntegrator;
using GameStateIntegrator.Entities;
using GameStateIntegrator.Extensions;
using System;
using System.Reactive.Linq;

Console.WriteLine("Hello, World!");

var listener = new GsListener(3000);
listener.StateEntityObserver
    .Only<Player>()
    .Subscribe(x => Console.WriteLine($"Player {x.Name}"));
listener.Start();

Console.ReadKey();