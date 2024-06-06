namespace EvaP2Correa
{
    public partial class RecargasDC : ContentPage
    {

        public RecargasDC()
        {
            InitializeComponent();
        }

        private int selectedAmount;

        // Validacion de monto seleccionado.
        private void ValorRecarga(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var radioButtonDC = sender as RadioButton;
                selectedAmount = int.Parse(radioButtonDC.Content.ToString().Split(' ')[0]);
                DisplayAlert("Monto de Saldo Seleccionado", $"Selecciono una recarga de ${selectedAmount} dolares", "Confirmar Recarga", "Cancelar");
            }
        }

        // Validacion de espacios en blanco & recarga exitosa
        private async void RecargarBotonDC(object sender, EventArgs e)
        {
            var numeroDC = IngresarCel.Text;
            var operadoraDC = OperadoraDCC.SelectedItem?.ToString();




            if (string.IsNullOrEmpty(numeroDC) || string.IsNullOrEmpty(operadoraDC) || selectedAmount == 0)
            {
                await DisplayAlert("Error", "Todos los campos deben estar llenos.", "Volver");
                return;
            }

            var result = await DisplayAlert("Confirmar Recarga", $"¿Desea recargar {selectedAmount} dolares al numero {numeroDC} con la operadora {operadoraDC}?", "Confirmar", "Cancelar");
            if (result)
            {
                await RealizarRecargaAsync(numeroDC, selectedAmount);
                await DisplayAlert("Recarga Realizada", $"Se hizo una recarga de {selectedAmount} dolares al numero {numeroDC}.", "Listo");
            }
        }
        // Guardado de datos en un archivo.txt
        private async Task RealizarRecargaAsync(string numeroDC, int amount)
        {
            var fecha = DateTime.Now.ToString("dd/MM/yyyy");
            var content = $"Se hizo una recarga de {amount} dolares el: {fecha}";
            var recargaArch = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{numeroDC}.txt");

            using (var writer = new StreamWriter(recargaArch))
            {
                await writer.WriteLineAsync(content);
            }
        }
    }

}
