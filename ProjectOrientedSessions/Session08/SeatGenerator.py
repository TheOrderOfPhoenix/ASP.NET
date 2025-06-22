vehicles = [
    {"id": 1, "title": "Volvo 9700", "type": 1, "capacity": 50},
    {"id": 2, "title": "Scania Touring", "type": 1, "capacity": 52},
    {"id": 3, "title": "TGV Duplex", "type": 2, "capacity": 510},
    {"id": 4, "title": "Shinkansen N700", "type": 2, "capacity": 1323},
    {"id": 5, "title": "Boeing 777", "type": 3, "capacity": 396},
    {"id": 6, "title": "Airbus A380", "type": 3, "capacity": 469}
]

def generate_seats(vehicle):
    seats = []
    vehicle_id = vehicle["id"]
    capacity = vehicle["capacity"]
    vtype = vehicle["type"]
    vip_rows = 3 if capacity >= 50 else 1

    if vtype == 1:  # Bus: 2+2 layout
        seats_per_row = 4
    elif vtype == 2:  # Train: 2+2 layout
        seats_per_row = 4
    elif vtype == 3:  # Airplane: 3+4+3 (approx 10 per row)
        seats_per_row = 10
    else:
        seats_per_row = 4  # default

    rows = (capacity + seats_per_row - 1) // seats_per_row

    seat_id = 1
    for row in range(1, rows + 1):
        for col in range(1, seats_per_row + 1):
            if seat_id > capacity:
                break
            seat = {
                "Id": seat_id,
                "VehicleId": vehicle_id,
                "Row": row,
                "Column": col,
                "IsVIP": row <= vip_rows,
                "IsAvailable": True,
                "Description": ""
            }
            seats.append(seat)
            seat_id += 1
    return seats

# Generate and print all
all_seats = []
for v in vehicles:
    seats = generate_seats(v)
    all_seats.extend(seats)

# Print as SQL INSERTs (optional)
for s in all_seats:
    print(f"INSERT INTO Seats (Id, VehicleId, Row, Column, IsVIP, IsAvailable, Description) VALUES "
          f"({s['Id']}, {s['VehicleId']}, {s['Row']}, {s['Column']}, {str(s['IsVIP']).lower()}, "
          f"{str(s['IsAvailable']).lower()}, '{s['Description']}');")
