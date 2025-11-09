1. run => dotnet ef migrations add InitialCreate
2. run => dotnet ef database update

3. run => docker build -t stock-management-system-service .
4. run => docker run -d -p 5179:5179 -v D:/db_stock:/app/data stick-management-system-service

หมายเหตุ: mount ทั้ง folder แทนที่จะ mount แค่ไฟล์เดียว

# Push to Docker Hub

5. run => docker login
6. run => docker tag stock-management-system-service nongoben331/stock-system:latest
7. run => docker push nongoben331/stock-system:latest
