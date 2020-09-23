import matplotlib.pyplot as plt 

x=[]
y=[]
color=[]

count=int(input())


for i in range(0, count):
        x.append(float(input()))
        y.append(float(input()))
        color.append(int(input()))

plt.scatter(x,y,c=color)
plt.show()
