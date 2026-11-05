
using mguerronS5.Models;

namespace mguerronS5.Views;

public partial class vPersona : ContentPage
{
    private int idPersonaSeleccionada = 0;

    public vPersona()
    {
        InitializeComponent();
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblMenssage.Text = "";

        App._personRepo.AddNewPerson(txtNombre.Text);
        lblMenssage.Text = App._personRepo.Message;

        txtNombre.Text = "";
        idPersonaSeleccionada = 0;

        MostrarPersonas();
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        lblMenssage.Text = "";
        MostrarPersonas();
    }

    private void MostrarPersonas()
    {
        List<Persona> people = App._personRepo.GetAllPerson();
        listaPersonas.ItemsSource = people;
    }

    private void listaPersonas_SelectionChanged(object sender,
        SelectionChangedEventArgs e)
    {
        Persona persona = e.CurrentSelection.FirstOrDefault() as Persona;

        if (persona != null)
        {
            idPersonaSeleccionada = persona.Id;
            txtNombre.Text = persona.Name;
        }
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        if (idPersonaSeleccionada == 0)
        {
            await DisplayAlert("Aviso",
                "Seleccione una persona para actualizar", "OK");
            return;
        }

        App._personRepo.UpdatePerson(idPersonaSeleccionada, txtNombre.Text);

        lblMenssage.Text = App._personRepo.Message;

        txtNombre.Text = "";
        idPersonaSeleccionada = 0;
        listaPersonas.SelectedItem = null;

        MostrarPersonas();
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (idPersonaSeleccionada == 0)
        {
            await DisplayAlert("Aviso",
                "Seleccione una persona para eliminar", "OK");
            return;
        }

        bool confirmar = await DisplayAlert(
            "Confirmar",
            "¿Está seguro de eliminar esta persona?",
            "Sí",
            "No");

        if (!confirmar)
            return;

        App._personRepo.DeletePerson(idPersonaSeleccionada);

        lblMenssage.Text = App._personRepo.Message;

        txtNombre.Text = "";
        idPersonaSeleccionada = 0;
        listaPersonas.SelectedItem = null;

        MostrarPersonas();
    }
}
