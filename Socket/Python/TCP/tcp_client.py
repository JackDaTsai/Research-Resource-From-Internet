'''
How to create a TCP socket client using Python
'''

import socket

HOST = '0.0.0.0'
PORT = 7000

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.connect((HOST, PORT))
    print(f'connected to server {HOST}:{PORT}')

    while True:
        outdata = input('please input message: ')
        print(f'send: {outdata}')
        s.send(outdata.encode())

        indata = s.recv(1024)
        if not indata: # connection closed
            print('server closed connection.')
            break
        print(f'recv: {indata.decode()}')