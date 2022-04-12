using Entities;

namespace StateContainer;

public class StateContainer
{
    
    // private string? savedString;
    private Product? savedProduct;
    public event Action OnChange;
    public Product Property
    {
        get => savedProduct ?? new Product();
        set
        {
            savedProduct = value;
            Console.WriteLine(value);
            NotifyStateChanged();
        }
    }

    
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}