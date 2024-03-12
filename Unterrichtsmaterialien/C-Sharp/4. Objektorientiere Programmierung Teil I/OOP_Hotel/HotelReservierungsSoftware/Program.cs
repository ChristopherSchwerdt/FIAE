class Room
{
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public double PricePerNight { get; set; }
    public bool IsBooked { get; set; }

    public Room(int roomNumber,string roomType,double pricePerNight)
    {
        RoomNumber = roomNumber;
        RoomType = roomType;
        PricePerNight = pricePerNight; 
    }
    

}

class Guest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Room BookedRoom { get; set; }
}

class HotelBookingSystem
{
    private List<Room> rooms = new List<Room>();
    private List<Guest> guests = new List<Guest>();

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    public bool IsRoomAvailable(int roomNumber, DateTime checkInDate, DateTime checkOutDate)
    {
        Room room = rooms.Find(r => r.RoomNumber == roomNumber);
        if (room != null)
        {
            return !room.IsBooked;
        }
        return false;
    }

    public void BookRoom(int roomNumber, Guest guest, DateTime checkInDate, DateTime checkOutDate)
    {
       
        Room room = rooms.Find(r => r.RoomNumber == roomNumber);
        if (room != null && !room.IsBooked)
        {
            room.IsBooked = true;
            guest.CheckInDate = checkInDate;
            guest.CheckOutDate = checkOutDate;
            guest.BookedRoom = room;
            guests.Add(guest);
            Console.WriteLine("Room booked successfully for guest: " + guest.Name);
        }
        else
        {
            Console.WriteLine("Room not available for booking.");
        }
    }

    public double CalculatePrice(Guest guest)
    {
        double totalCost = (guest.CheckOutDate - guest.CheckInDate).TotalDays * guest.BookedRoom.PricePerNight;
        return totalCost;
    }

    public void DisplayBookedRooms()
    {
        foreach (Guest guest in guests)
        {
            Console.WriteLine("Guest: " + guest.Name + " - Room Number: " + guest.BookedRoom.RoomNumber);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        HotelBookingSystem hotel = new HotelBookingSystem();

        Room singleRoom = new Room (101, "Single", 50.0);
        Room doubleRoom = new Room (201, "Double", 80.0);

        hotel.AddRoom(singleRoom);
        hotel.AddRoom(doubleRoom);

        Guest guest1 = new Guest { Id = 1, Name = "Alice" };
        Guest guest2 = new Guest { Id = 2, Name = "Bob" };

        hotel.BookRoom(101, guest1, new DateTime(2024, 10, 1), new DateTime(2024, 10, 5));
        hotel.BookRoom(201, guest2, new DateTime(2024, 12, 1), new DateTime(2024, 12, 10));
    }
}
