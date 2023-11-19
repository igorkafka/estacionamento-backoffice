using EstacionamentoBackoffice.Business.Models;
using EstacionamentoBackoffice.Domain.Tests.Fixtures;
using System.Reflection;

namespace EstacionamentoBackoffice.Domain.Tests
{
    [Collection(nameof(PassagemCollection))]
    public class PassagemTests
    {
        private readonly PassagemValidoTestsFixture _passagemValidoTestsFixture;

        public PassagemTests(PassagemValidoTestsFixture passagemValidoTestsFixture)
        {
            _passagemValidoTestsFixture = passagemValidoTestsFixture;
        }

        [Fact(DisplayName = "Realizar Passagem de Menos de Uma Hora")]
        [Trait("Categoria", "Passagem")]
        public void RealizarPassagem_SemCarencia_DeveCalcularPrecoTotal()
        {
            // Arrange
            var passagem = _passagemValidoTestsFixture.GerarPassagemValida();
            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = new DateTime(2023, 4, 9, 14, 30, 0);
            // Act
            passagem.CalcularPrecoTotal();

            // Assert
            Assert.Equal(42, passagem.PrecoTotal);
        }
        [Fact(DisplayName = "Realizar Passagem de Menos de Uma Hora")]
        [Trait("Categoria", "Passagem")]
        public void RealizarPassagem_DataHoraSaidaIgualNulo_DeveCalcularPrecoTotal()
        {
            // Arrange
            var passagem = _passagemValidoTestsFixture.GerarPassagemValida();
            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = null;
            // Act
            passagem.CalcularPrecoTotal();

            // Assert
            Assert.Equal(42, passagem.PrecoTotal);
        }
        [Fact(DisplayName = "Realizar Passagem de mais de Uma Hora I")]
        [Trait("Categoria", "Passagem")]
        public void RealizarPassagem_ComCarencia_DeveCalcularPrecoTotalI()
        {
            // Arrange
            var passagem = _passagemValidoTestsFixture.GerarPassagemValida();
            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = new DateTime(2023, 4, 9, 15, 15, 0);
            // Act
            passagem.CalcularPrecoTotal();

            // Assert
            Assert.Equal(47, passagem.PrecoTotal);
        }
        [Fact(DisplayName = "Realizar Passagem de mais de Uma Hora II")]
        [Trait("Categoria", "Passagem")]
        public void RealizarPassagem_ComCarencia_DeveCalcularPrecoTotalII()
        {
            // Arrange
            var passagem = _passagemValidoTestsFixture.GerarPassagemValida();
            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = new DateTime(2023, 4, 9, 15, 30, 0);
            // Act
            passagem.CalcularPrecoTotal();

            // Assert
            Assert.Equal(52, passagem.PrecoTotal);
        }
        [Fact(DisplayName = "Realizar Passagem de mais de Uma Hora III")]
        [Trait("Categoria", "Passagem")]
        public void RealizarPassagem_ComCarencia_DeveCalcularPrecoTotalIII()
        {
            // Arrange
            var passagem = _passagemValidoTestsFixture.GerarPassagemValida();
            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = new DateTime(2023, 4, 9, 16, 30, 0);
            // Act
            passagem.CalcularPrecoTotal();

            // Assert
            Assert.Equal(62, passagem.PrecoTotal);
        }
        [Fact(DisplayName = "Realizar Passagem de mais de Uma Hora IV"), TestPriority(1)]
        [Trait("Categoria", "Passagem")]
        public void RealizarPassagem_ComCarencia_DeveCalcularPrecoTotalIV()
        {
            // Arrange
            var passagem = _passagemValidoTestsFixture.GerarPassagemValida();
            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = new DateTime(2023, 4, 9, 16, 31, 0);
            // Act
            passagem.CalcularPrecoTotal();

            // Assert
            Assert.Equal(67, passagem.PrecoTotal);
        }
        [Fact(DisplayName = "Realizar Passagem Mensalista"), TestPriority(1)]
        [Trait("Categoria", "Passagem")]
        public void RealizarPassagem_DeveCalcularMensalista()
        {
            var passagem = _passagemValidoTestsFixture.GerarPassagemMensalistaValida();

            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = new DateTime(2023, 4, 9, 16, 31, 0);
            // Act
            passagem.CalcularPrecoTotal();

            // Assert
            Assert.Equal(passagem.PrecoTotal, 550);
        }
        [Fact(DisplayName = "Calcular Diferença da Entrada e Saída de Uma Passagem")]
        [Trait("Categoria", "Passagem")]
        public void CalcularEstadiaEmMinutos_DeveSerIgual()
        {

            // get the type
            Type type = typeof(Passagem);

            // create the object of the type
            dynamic passagem = Activator.CreateInstance(type);
            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = new DateTime(2023, 4, 9, 15, 15, 0);

            // get the private method
            PropertyInfo prop =
                 type.GetProperty("CalcularEstadiaEmMinutos", BindingFlags.NonPublic | BindingFlags.Instance);

            // prepare parameters object accepted by the private method
            MethodInfo getter = prop.GetGetMethod(nonPublic: true);
            object result = getter.Invoke(passagem, null);

            // verify the result
            Assert.Equal(result, Convert.ToDouble(105));
        }
        [Fact(DisplayName = "Calcular Diferença da Entrada e Saída de Uma Passagem")]
        [Trait("Categoria", "Passagem")]
        public void CalcularEstadiaEmMinutos_NaoDeveIgual()
        {

            // get the type
            Type type = typeof(Passagem);

            // create the object of the type
            dynamic passagem = Activator.CreateInstance(type);
            passagem.DataHoraEntrada = new DateTime(2023, 4, 9, 13, 30, 0);
            passagem.DataHoraSaida = new DateTime(2023, 4, 9, 15, 15, 0);

            // get the private method
            PropertyInfo prop =
                 type.GetProperty("CalcularEstadiaEmMinutos", BindingFlags.NonPublic | BindingFlags.Instance);

            // prepare parameters object accepted by the private method
            MethodInfo getter = prop.GetGetMethod(nonPublic: true);
            object result = getter.Invoke(passagem, null);

            // verify the result
            Assert.NotEqual(result, 105);
        }
    }
}
