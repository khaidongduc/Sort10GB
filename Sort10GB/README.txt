- Chia dữ liêun ban đầu thành 20 phần, ghi 20 phần ra hard drive
- Cho từng phần vào memory rồi sort dùng quick sort
- Lấy số đầu tiên của 20 phần, ghi số nhỏ nhất vào file kết quả. Làm cho đến khi hết dữ liệu

- Dùng cách này chỉ cần dùng tối đa 1/20 của 10GB là 500MB memory khi đọc file vào memory
- Khi chuyển sang List<int> thì sẽ chỉ còn 243-250MB
- Khi sort thì chỉ dùng O(logn) memory


Diagnoses:

CPU: AMD Ryzen 5 3500U with	Radeon Vega Mobile Gfx
RAM used: 873MB
Hard Drive Type: HDD

Generating Test...
Time Elapsed 00:02:22.1295247

Splitting File
Time Elapsed 00:06:37.8633655

Sort All Temp Files.
Time Elapsed 00:19:31.8198629

Min Mixing All Temp Files.
Time Elapsed 00:28:02.8978774

Cleaning...
Time Elapsed 00:28:03.0682745

Checking...
Time Elapsed 00:31:13.0635558

Successfully Sorted the Data