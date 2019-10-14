using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    abstract class Señal
    {
        public List<Muestra> Muestras { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaMuestreo { get; set; }
        public double AmplitudMaxima { get; set; }
        public abstract double evaluar(double tiempo);
        public void construirSeñal()
        {
            double periodoMuestreo = 1 / FrecuenciaMuestreo;
            Muestras.Clear();
            for (double i = TiempoInicial; i <= TiempoFinal; i += periodoMuestreo)
            {
                double muestra = evaluar(i);
                Muestras.Add(new Muestra(i, muestra));

                if (Math.Abs(muestra) > AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(muestra);
                }
            }
        }

        public static Señal escalaExponenecial(Señal señalOriginal, double exponente)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo = señalOriginal.FrecuenciaMuestreo;

            foreach(var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = Math.Pow(muestra.Y, exponente);
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));
                if(Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }
            return resultado;

        }
        public static Señal escalarAmplitud(Señal señalOriginal, double factorEscala)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo = señalOriginal.FrecuenciaMuestreo;

            foreach (var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = muestra.Y * factorEscala;
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));
                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }
            return resultado;
        }
        public static Señal desplazamientoAmplitud(Señal señalOriginal, double cantidadDesplazamiento)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo = señalOriginal.FrecuenciaMuestreo;

            foreach (var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = muestra.Y + cantidadDesplazamiento;
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));
                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }
            return resultado;
        }
        public static Señal limiteSuperior(Señal señal1, Señal señal2)
        {
            SeñalResultante resultado = new SeñalResultante();

            /*SeñalResultante resultado1 = new SeñalResultante();
            SeñalResultante resultado2 = new SeñalResultante();*/

            resultado.TiempoInicial = señal1.TiempoInicial;
            resultado.TiempoFinal = señal1.TiempoFinal;
            resultado.FrecuenciaMuestreo = señal1.FrecuenciaMuestreo;

            /*resultado1.TiempoInicial = señal1.TiempoInicial;
            resultado1.TiempoFinal = señal1.TiempoFinal;
            resultado1.FrecuenciaMuestreo = señal1.FrecuenciaMuestreo;
            resultado2.TiempoInicial = señal2.TiempoInicial;
            resultado2.TiempoFinal = señal2.TiempoFinal;
            resultado2.FrecuenciaMuestreo = señal2.FrecuenciaMuestreo;*/
            int indice = 0;
            foreach (var muestra in señal1.Muestras)
            {
                double nuevoValor = muestra.Y;
                if (muestra.Y >= señal2.Muestras[indice].Y)
                    resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));
                else
                    resultado.Muestras.Add(new Muestra(muestra.X, señal2.Muestras[indice].Y));

                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
                indice++;
            }


            /*foreach (var muestra in señal1.Muestras)
            { 
                resultado1.Muestras.Add(new Muestra(muestra.X, muestra.Y));

                if (Math.Abs(muestra.Y) > resultado1.AmplitudMaxima)
                {
                    resultado1.AmplitudMaxima  = Math.Abs(muestra.Y);
                }
            }
            foreach(var muestra in señal2.Muestras)
            {
                double nuevoValor = muestra.Y;
                resultado2.Muestras.Add(new Muestra(muestra.X, nuevoValor));

                if (Math.Abs(nuevoValor) > resultado2.AmplitudMaxima)
                {
                    resultado2.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }
            if(resultado1.AmplitudMaxima >= resultado2.AmplitudMaxima)
                return resultado1;
            else
                return resultado2;*/
            return resultado;
        }
        public static Señal multiplicarSeñales(Señal señal1, Señal señal2)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señal1.TiempoInicial;
            resultado.TiempoFinal = señal1.TiempoFinal;
            resultado.FrecuenciaMuestreo = señal1.FrecuenciaMuestreo;
            int indice = 0;
            foreach(var muestra in señal1.Muestras)
            {
                double nuevoValor = muestra.Y * señal2.Muestras[indice].Y;
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));

                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
                indice++;
            }
            return resultado;
        }
        
    }
}
