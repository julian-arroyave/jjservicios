namespace JJServicios.DB.Contracts.Repositories
{
    public interface IDeleteAdoRepository
    {
        void DeleteItemById(long id, string itemName);

    }
}
