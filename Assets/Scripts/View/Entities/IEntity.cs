namespace View.Entities {
    public interface IEntity {

        void Attack();
        
        void TakeDamage(int damage);
        
        void Die();

    }
}