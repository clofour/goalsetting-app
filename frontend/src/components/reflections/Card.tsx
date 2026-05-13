import { useGetApiReflectionList } from '@/api/endpoints/reflection/reflection';
import { Paper, SimpleGrid, Stack, Text } from '@mantine/core';

interface CardProps {
    aspect: string;
    date: string;
    goal: string;
}

export default function Card({aspect, date, goal}: CardProps) {
    return (
        <Paper>
           <Stack>
                {aspect}
                {goal} x {date}
            </Stack> 
        </Paper>
    );
}
