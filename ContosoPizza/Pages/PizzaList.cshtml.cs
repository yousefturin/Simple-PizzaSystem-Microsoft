using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// Import the Pizza and PizzaService 
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    // Variable that will hold a reference to a PizzaService object.
public class PizzaListModel : PageModel
{
    // Delete method is called when a user is clicks delete button for pizza.
    public IActionResult OnPostDelete(int id)
    {
    _service.DeletePizza(id);

    return RedirectToAction("Get");
    }
    
    // This is used to blind the NewPizza propert to Razor page when a http post request is made.
    [BindProperty]
    // NewPizza property is added to PizzaListModel class.
    public Pizza NewPizza { get; set; } = default!;
    public IActionResult OnPost()
    {   // Check if the user's input is correct, if correct re-render the page
        if (!ModelState.IsValid || NewPizza == null)
        {
            return Page();
        }
        // NewPizza is used to add new pizza to the _service.
        _service.AddPizza(NewPizza);
        // redirect tthe user to Get page handler, which must re-render a page with updated list of pizzas.
        return RedirectToAction("Get");
    }
    // Readonly is used to indicate that the value of _service cant be changed after it's set in the constructor.
    private readonly PizzaService _service;
    // Pizza list is defined to hold the list of pizza. and PizzaList is set so null safety checks aren't requried
    public IList<Pizza> PizzaList { get;set; } = default!;
    // The PizzaService is provided by dependency injection.
    public PizzaListModel(PizzaService service)
    {
        _service = service;
    }
    // Retrieve the list of pizza from the PizzaService and then store it in PizzaList.
    public void OnGet()
    {
        PizzaList = _service.GetPizzas();
    }
}
}
