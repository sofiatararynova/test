namespace Lab.Interfaces;

public interface ICreditCardValidator
{
    bool IsValid(string cardNumber);
}

//public interface IToDoListManager
//{
//    void AddTask(string description);
//    void RemoveTask(int id);
//    void MarkTaskAsCompleted(int id);
//    string[] GetTasks();
//}