public interface IResettable
{
    void ResetGameObject();
}

interface IPoolable
{
    void InitializePool();
    void DeployObject();
}