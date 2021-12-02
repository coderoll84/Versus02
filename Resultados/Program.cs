// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Resultados;

var summary = BenchmarkRunner.Run<Results>();
