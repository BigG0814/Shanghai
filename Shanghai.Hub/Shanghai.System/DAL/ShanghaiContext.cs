using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Shanghai.Data.Entities;

namespace Shanghai.System.DAL
{

    public class ShanghaiContext : DbContext
    {
        public ShanghaiContext()
            : base("name=DataEntities")
        {
        }

        public virtual DbSet<ComboItemSelection> ComboItemSelections { get; set; }
        public virtual DbSet<BillPayment> BillPayments { get; set; }
        public virtual DbSet<WorkHour> WorkHours { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<BillItem> BillItems { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<CellularProvider> CellularProviders { get; set; }
        public virtual DbSet<DailySale> DailySales { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<JobType> JobTypes { get; set; }
        public virtual DbSet<MenuCategory> MenuCategories { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<ShiftTrade> ShiftTrades { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<TimeOffRequest> TimeOffRequests { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }
        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ShanghaiContext>(null);
            modelBuilder.Entity<BillItem>()
                .Property(e => e.SellingPrice)
                .HasPrecision(10, 4);

            modelBuilder.Entity<BillItem>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .Property(e => e.SubTotal)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Bill>()
                .Property(e => e.GST)
                .HasPrecision(6, 4);

            modelBuilder.Entity<Bill>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .HasMany(e => e.BillItems)
                .WithRequired(e => e.Bill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bill>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.Bill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CellularProvider>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CellularProvider>()
                .Property(e => e.CellEmail)
                .IsUnicode(false);

            modelBuilder.Entity<DailySale>()
                .Property(e => e.Tip)
                .HasPrecision(10, 4);

            modelBuilder.Entity<DailySale>()
                .Property(e => e.SaleTotal)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Street)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Province)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PostalCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.HomePhone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CellPhone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.ContactPhone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.ContactRelation)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.ContactName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.DailySales)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Shifts)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.DeliveryDriverID);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ShiftTrades)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ApprovingEmployee);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ShiftTrades1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.NewEmployee);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ShiftTrades2)
                .WithRequired(e => e.Employee2)
                .HasForeignKey(e => e.OriginalEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TimeOffRequests)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<JobType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<JobType>()
                .HasMany(e => e.Shifts)
                .WithRequired(e => e.JobType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuCategory>()
                .HasMany(e => e.MenuItems)
                .WithRequired(e => e.MenuCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.MenuItemName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.CurrentPrice)
                .HasPrecision(10, 4);

            modelBuilder.Entity<MenuItem>()
                .HasMany(e => e.BillItems)
                .WithRequired(e => e.MenuItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuItem>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.MenuItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.SellingPrice)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.Fname)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.LName)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Street)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.SpecialInstructions)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.GST)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.DeliverFee)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.Tip)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OrderType>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.OrderType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shift>()
                .HasMany(e => e.ShiftTrades)
                .WithRequired(e => e.Shift)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Table>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Table)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TimeOffRequest>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<PaymentType>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.PaymentType)
                .WillCascadeOnDelete(false);


        }
    }
}
