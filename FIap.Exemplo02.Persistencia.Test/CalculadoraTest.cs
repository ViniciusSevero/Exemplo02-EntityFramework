using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FIap.Exemplo02.Persistencia.Test
{
    [TestClass]
    public class CalculadoraTest
    {
        [TestMethod]
        public void Calcular_Fatorial_OK()
        {
            //inicializar os objetos
            Calculadora calc = new Calculadora();
            //cHAMAR O MÉTODO QUE SERÁ TESTADO
            var resultado = calc.Fatorial(5);
            //validar resultado obtido
            Assert.AreEqual(120, resultado);
        }

        [TestMethod]
        public void Calcular_Fatorial_Zero()
        {
            Calculadora calc = new Calculadora();
            //cHAMAR O MÉTODO QUE SERÁ TESTADO
            var resultado = calc.Fatorial(0);
            //validar resultado obtido
            Assert.AreEqual(1, resultado);
        }
    }
}
