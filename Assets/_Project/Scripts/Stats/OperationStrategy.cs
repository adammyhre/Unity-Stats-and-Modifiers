public interface IOperationStrategy {
    int Calculate(int value);
}

public class AddOperation : IOperationStrategy {
    readonly int value;
    
    public AddOperation(int value) {
        this.value = value;
    }
    
    public int Calculate(int value) => value + this.value;
}

public class MultiplyOperation : IOperationStrategy {
    readonly int value;
    
    public MultiplyOperation(int value) {
        this.value = value;
    }
    
    public int Calculate(int value) => value * this.value;
}