public interface ICrud <T>
{
    List<T> Listing();
    T GetById(int id);
    bool Save(T t);
    T Delete(int id);
}