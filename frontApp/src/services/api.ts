const API_URL = 'https://api.escuelajs.co/api/v1/products'; 

export const getProducts = async () => {
  try {
    const response = await fetch(API_URL);
    if (!response.ok) {
      throw new Error('Error al obtener los productos');
    }
    const data = await response.json();
    return data;
  } catch (error) {
    console.error('Error en la solicitud:', error);
    throw error;
  }
};