namespace ToDoList.Domain.Abstractions;

public class Entity : IEntity
{
    private int? _requestedHashCode;

    public virtual int Id { get; set; }

    public bool IsTransient()
    {
        return Id == default(int);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Entity))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        var item = (Entity)obj;

        if (item.IsTransient() || IsTransient())
            return false;
        return item.Id == Id;
    }

    public override int GetHashCode()
    {
        if (IsTransient()) return base.GetHashCode();

        if (!_requestedHashCode.HasValue)
            _requestedHashCode = Id.GetHashCode() ^ 31;

        return _requestedHashCode.Value;
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return Equals(left, null) ? (Equals(right, null)) : left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
