'''
How to create a UDP socket client using Python
'''

import socket

HOST = '0.0.0.0'
PORT = 7000

with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:
    s.bind((HOST, PORT))
    print(f'server start at: {HOST}:{PORT}')

    while True:
        data, addr = s.recvfrom(1024)
        print(f'Received from {addr}: {data.decode()}')
        outdata = f'echo {data.decode()}'
        s.sendto(outdata.encode(), addr)