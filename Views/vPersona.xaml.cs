using mguerronS5.Models;

namespace mguerronS5.Views;

public partial class vPersona : ContentPage
{
	public vPersona()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblMenssage.Text = "";
        App._personRepo.AddNewPerson(txtNombre.Text);
        lblMenssage.Text = App._personRepo.Message;
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        lblMenssage.Text = "";
        List<Persona> people = App._personRepo.GetAllPerson();
        listaPersonas.ItemsSource = people;
    }
}