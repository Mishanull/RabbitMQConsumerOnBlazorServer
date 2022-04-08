using Entities;

namespace BlazorWithRabbitMQ;

public class StateContainer
{
    
    // private string? savedString;
    private Product? savedProduct;
    public Product Property
    {
        get => savedProduct ?? new Product();
        set
        {
            savedProduct = value;
            NotifyStateChanged();
        }
    }

    public event Action OnChange;

    private void NotifyStateChanged() => 
        OnChange?.Invoke();
}