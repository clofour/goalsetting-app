import { Schedule } from '@mantine/schedule';
import { useGetApiEventGet } from '@/api/endpoints/event/event';

export default function Calendar() {
    const { data: response, error, isLoading, mutate } = useGetApiEventGet();

    const events = response?.data.map((event) => ({
        ...event,
        color: "blue"
    }));

    return (
        <>
            {events && (
                <Schedule
                    events={events}
                    layout="responsive"
                />
            )}
        </>
    );
}

