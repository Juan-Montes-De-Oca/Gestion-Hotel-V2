using System;
using System.Collections.Generic;

List<Room> rooms = new List<Room>();

int option;
bool isValidOption;

do
{
    Console.Clear();
    Console.WriteLine("==================================================");
    Console.WriteLine("     SISTEMA DE GESTIÓN DE HABITACIONES (POO)     ");
    Console.WriteLine("==================================================");
    Console.WriteLine("1. Registrar nueva habitación");
    Console.WriteLine("2. Listar todas las habitaciones");
    Console.WriteLine("3. Buscar habitación");
    Console.WriteLine("4. Modificar habitación");
    Console.WriteLine("5. Eliminar habitación");
    Console.WriteLine("0. Salir del programa");
    Console.WriteLine("==================================================");
    Console.WriteLine("Elija una opción correspondiente (0 - 5): ");

    isValidOption = int.TryParse(Console.ReadLine(), out option);

    if (!isValidOption)
    {
        Console.WriteLine("\nERROR! Opción inválida. Introduzca un número del 0 al 5.");
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
        continue;
    }

    switch (option)
    {
        case 1:
            Console.WriteLine("\n=== REGISTRAR NUEVA HABITACIÓN ===");
            
            int roomNum;
            while (true)
            {
                Console.WriteLine("Ingrese el número de la habitación (Ej. 101):");
                string inputNum = (Console.ReadLine() ?? "").Trim();

                if (!int.TryParse(inputNum, out roomNum) || roomNum <= 0)
                {
                    Console.WriteLine("ERROR! El número de habitación debe ser un entero positivo.");
                    continue;
                }

                if (rooms.Exists(h => h.Number == roomNum))
                {
                    Console.WriteLine($"ERROR! La habitación #{roomNum} ya se encuentra registrada. Ingrese otra.");
                    continue;
                }
                break;
            }

            string roomType = "";
            while (true)
            {
                Console.WriteLine("\nSeleccione el tipo de habitación:");
                Console.WriteLine("1. Individual (Simple)");
                Console.WriteLine("2. Doble");
                Console.WriteLine("3. Matrimonial");
                Console.WriteLine("4. Suite Junior");
                Console.WriteLine("5. Suite Presidencial");
                Console.WriteLine("Elija una opción (1 - 5):");

                if (int.TryParse(Console.ReadLine(), out int typeChoice))
                {
                    switch (typeChoice)
                    {
                        case 1: roomType = "Individual (Simple)"; break;
                        case 2: roomType = "Doble"; break;
                        case 3: roomType = "Matrimonial"; break;
                        case 4: roomType = "Suite Junior"; break;
                        case 5: roomType = "Suite Presidencial"; break;
                    }
                    if (!string.IsNullOrEmpty(roomType)) break;
                }
                Console.WriteLine("ERROR! Opción de tipo inválida. Intente de nuevo.");
            }

            decimal roomPrice;
            while (true)
            {
                Console.WriteLine("\nIngrese el precio por noche ($):");
                string inputPrice = (Console.ReadLine() ?? "").Trim();

                if (decimal.TryParse(inputPrice, out roomPrice) && roomPrice >= 0)
                {
                    break;
                }
                Console.WriteLine("ERROR! El precio debe ser un valor numérico válido mayor o igual a 0.");
            }

            string roomStatusVal = "";
            while (true)
            {
                Console.WriteLine("\nSeleccione el estado de la habitación:");
                Console.WriteLine("1. Disponible");
                Console.WriteLine("2. Ocupada");
                Console.WriteLine("3. Mantenimiento");
                Console.WriteLine("4. Limpieza");
                Console.WriteLine("Elija una opción (1 - 4):");

                if (int.TryParse(Console.ReadLine(), out int statusChoice))
                {
                    switch (statusChoice)
                    {
                        case 1: roomStatusVal = "Disponible"; break;
                        case 2: roomStatusVal = "Ocupada"; break;
                        case 3: roomStatusVal = "Mantenimiento"; break;
                        case 4: roomStatusVal = "Limpieza"; break;
                    }
                    if (!string.IsNullOrEmpty(roomStatusVal)) break;
                }
                Console.WriteLine("ERROR! Opción de estado inválida. Intente de nuevo.");
            }

            Console.WriteLine("\nIngrese detalles o ubicación (Ej. Vista al mar, Piso 2):");
            string details = (Console.ReadLine() ?? "").Trim();
            if (string.IsNullOrWhiteSpace(details)) details = "Estándar";

            Room newRoom = new Room(roomNum, roomType, roomPrice, roomStatusVal, details);
            rooms.Add(newRoom);

            Console.WriteLine($"\n¡Éxito! Habitación #{roomNum} registrada correctamente como objeto.");
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
            break;

        case 2:
            Console.WriteLine("\n=== LISTADO GENERAL DE HABITACIONES ===");
            if (rooms.Count == 0)
            {
                Console.WriteLine("No hay habitaciones registradas en el sistema.");
            }
            else
            {
                for (int i = 0; i < rooms.Count; i++)
                {
                    Console.Write($"[{i + 1}] ");
                    rooms[i].ShowDetails();
                }
                Console.WriteLine($"\nTotal de habitaciones registradas: {rooms.Count}");
            }
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
            break;

        case 3:
            Console.WriteLine("\n=== BUSCAR HABITACIÓN ===");
            if (rooms.Count == 0)
            {
                Console.WriteLine("No hay habitaciones registradas para buscar.");
                Console.WriteLine("\nPresione Enter para continuar...");
                Console.ReadLine();
                break;
            }

            Console.WriteLine("Ingrese el número de la habitación a buscar:");
            if (int.TryParse(Console.ReadLine(), out int searchNum))
            {
                Room? foundRoom = rooms.Find(h => h.Number == searchNum);
                if (foundRoom != null)
                {
                    Console.WriteLine($"\n[¡Encontrada!]");
                    foundRoom.ShowDetails();
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna habitación con ese número.");
                }
            }
            else
            {
                Console.WriteLine("ERROR! Entrada inválida. Debe ingresar un número entero.");
            }
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
            break;

        case 4:
            Console.WriteLine("\n=== MODIFICAR HABITACIÓN ===");
            if (rooms.Count == 0)
            {
                Console.WriteLine("No hay habitaciones registradas para modificar.");
                Console.WriteLine("\nPresione Enter para continuar...");
                Console.ReadLine();
                break;
            }

            Console.WriteLine("Ingrese el número de la habitación que desea modificar:");
            if (int.TryParse(Console.ReadLine(), out int modNum))
            {
                Room? roomToModify = rooms.Find(h => h.Number == modNum);
                if (roomToModify != null)
                {
                    Console.WriteLine($"\nDatos actuales -> Precio: ${roomToModify.Price:F2} | Estado: {roomToModify.Status}");
                    
                    Console.WriteLine("Ingrese el nuevo precio (o presione Enter para mantener el actual):");
                    string priceInput = (Console.ReadLine() ?? "").Trim();
                    if (priceInput != "")
                    {
                        if (decimal.TryParse(priceInput, out decimal newPrice) && newPrice >= 0)
                        {
                            roomToModify.Price = newPrice;
                            Console.WriteLine("-> Precio actualizado con éxito.");
                        }
                        else
                        {
                            Console.WriteLine("-> ERROR: Precio inválido. Se mantuvo el precio anterior.");
                        }
                    }

                    Console.WriteLine("\n¿Desea cambiar el estado de la habitación? (1. Sí / 2. No):");
                    if (int.TryParse(Console.ReadLine(), out int changeStatusChoice) && changeStatusChoice == 1)
                    {
                        string newStatusVal = "";
                        while (true)
                        {
                            Console.WriteLine("Seleccione el nuevo estado:");
                            Console.WriteLine("1. Disponible");
                            Console.WriteLine("2. Ocupada");
                            Console.WriteLine("3. Mantenimiento");
                            Console.WriteLine("4. Limpieza");
                            Console.WriteLine("Elija una opción (1 - 4):");

                            if (int.TryParse(Console.ReadLine(), out int statusChoice))
                            {
                                switch (statusChoice)
                                {
                                    case 1: newStatusVal = "Disponible"; break;
                                    case 2: newStatusVal = "Ocupada"; break;
                                    case 3: newStatusVal = "Mantenimiento"; break;
                                    case 4: newStatusVal = "Limpieza"; break;
                                }
                                if (!string.IsNullOrEmpty(newStatusVal))
                                {
                                    roomToModify.Status = newStatusVal;
                                    Console.WriteLine("-> Estado actualizado con éxito.");
                                    break;
                                }
                            }
                            Console.WriteLine("ERROR! Opción inválida. Intente de nuevo.");
                        }
                    }

                    Console.WriteLine("\n¡Proceso de modificación finalizado!");
                }
                else
                {
                    Console.WriteLine("No existe una habitación registrada con ese número.");
                }
            }
            else
            {
                Console.WriteLine("ERROR! Entrada inválida. Debe ingresar un número entero.");
            }
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
            break;

        case 5:
            Console.WriteLine("\n=== ELIMINAR HABITACIÓN ===");
            if (rooms.Count == 0)
            {
                Console.WriteLine("No hay habitaciones registradas para eliminar.");
                Console.WriteLine("\nPresione Enter para continuar...");
                Console.ReadLine();
                break;
            }

            Console.WriteLine("Ingrese el número de la habitación que desea eliminar:");
            if (int.TryParse(Console.ReadLine(), out int delNum))
            {
                Room? roomToDelete = rooms.Find(h => h.Number == delNum);
                if (roomToDelete != null)
                {
                    Console.WriteLine($"¿Está seguro de eliminar la habitación #{roomToDelete.Number}? (S/N):");
                    string confirm = (Console.ReadLine() ?? "").Trim().ToUpper();

                    if (confirm == "S")
                    {
                        rooms.Remove(roomToDelete);
                        Console.WriteLine("\n¡Habitación eliminada correctamente del sistema!");
                    }
                    else
                    {
                        Console.WriteLine("\nOperación cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna habitación con ese número.");
                }
            }
            else
            {
                Console.WriteLine("ERROR! Entrada inválida. Debe ingresar un número entero.");
            }
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
            break;

        case 0:
            Console.WriteLine("\n=== GRACIAS POR UTILIZAR NUESTRO SISTEMA. HASTA PRONTO! ===");
            break;

        default:
            Console.WriteLine("\nERROR! Opción fuera de rango (Elija entre 0 y 5).");
            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
            break;
    }
}
while (option != 0);