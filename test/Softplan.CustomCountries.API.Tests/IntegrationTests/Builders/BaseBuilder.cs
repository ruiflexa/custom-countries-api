namespace Softplan.CustomCountries.API.Tests.IntegrationTests.Builders
{
    public abstract class BaseBuilder<T, M>
    {
        protected M Model;

        public static implicit operator M(BaseBuilder<T, M> instance)
        {
            return instance.Build();
        }

        public M Build()
        {
            return Model;
        }
    }
}
