'''
How to create a TCP socket server using Python
'''

import socket

HOST = '0.0.0.0'
PORT = 7000

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen()

    print(f'server start at: {HOST}:{PORT}')
    print('wait for connection...')

    conn, addr = s.accept()
    print(f'connected by {addr}')

    with conn:
        while True:
            data = conn.recv(1024)
            if not data:
                print('client closed connection.')
                break
            print(f'recv: {data.decode()}')
            conn.sendall(f'echo {data.decode()}'.encode())